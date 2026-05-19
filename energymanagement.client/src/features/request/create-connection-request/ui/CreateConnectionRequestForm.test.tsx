/**
 * @vitest environment jsdom
 */
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { cleanup, render, screen, waitFor } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { afterEach, describe, expect, it, vi } from "vitest";
import type { ApplicantPartySummary } from "../../../../entities/applicant-party/model/applicantPartyTypes";
import { createConnectionRequest } from "../api/createConnectionRequest";
import { CreateConnectionRequestForm } from "./CreateConnectionRequestForm";

vi.mock("../api/createConnectionRequest", () => ({
  createConnectionRequest: vi.fn(),
  __esModule: true,
}));

const mockedCreateConnectionRequest = vi.mocked(createConnectionRequest);

const renderForm = (applicantParties: ApplicantPartySummary[]) => {
  const queryClient = new QueryClient({
    defaultOptions: {
      queries: { retry: false },
      mutations: { retry: false },
    },
  });
  const onSuccess = vi.fn();

  render(
    <QueryClientProvider client={queryClient}>
      <CreateConnectionRequestForm
        applicantParties={applicantParties}
        onSuccess={onSuccess}
      />
    </QueryClientProvider>,
  );

  return { onSuccess };
};

const applicantParties: ApplicantPartySummary[] = [
  {
    applicantPartyId: 1,
    applicantPartyType: "Individual",
    displayName: "Ivan Ivanov",
    fullName: {
      firstName: "Ivan",
      middleName: "Ivanovich",
      lastName: "Ivanov",
    },
    email: "ivan@example.com",
    phoneNumber: "+79001234567",
    verificationStatus: "Unverified",
    isCurrentDefault: true,
    createdAt: "2026-01-01T10:00:00Z",
  },
  {
    applicantPartyId: 2,
    applicantPartyType: "Individual",
    displayName: "Petr Petrov",
    fullName: {
      firstName: "Petr",
      middleName: "Petrovich",
      lastName: "Petrov",
    },
    email: "petr@example.com",
    phoneNumber: "+79007654321",
    verificationStatus: "Unverified",
    isCurrentDefault: false,
    createdAt: "2026-01-02T10:00:00Z",
  },
];

const fillRequestFields = async () => {
  const user = userEvent.setup();

  await user.type(
    screen.getByLabelText("Описание заявки"),
    "Подключение объекта к электрическим сетям",
  );
  await user.type(screen.getByLabelText("Почтовый индекс"), "658480");
  await user.type(screen.getByLabelText("Регион"), "Алтайский край");
  await user.type(screen.getByLabelText("Город"), "Заринск");
  await user.type(screen.getByLabelText("Улица"), "Ленина");
  await user.type(screen.getByLabelText("Дом"), "10");

  return user;
};

describe("CreateConnectionRequestForm", () => {
  afterEach(() => {
    cleanup();
    mockedCreateConnectionRequest.mockReset();
  });

  it("selects the current/default ApplicantParty first", () => {
    renderForm(applicantParties);

    expect(screen.getByLabelText("Сохранённый заявитель")).toHaveValue("1");
    expect(screen.getByText(/Ivan Ivanov/)).toBeVisible();
    expect(screen.getByText(/Текущий/)).toBeVisible();
  });

  it("submits the Existing branch with a selected non-default ApplicantParty", async () => {
    mockedCreateConnectionRequest.mockResolvedValue(undefined);
    const { onSuccess } = renderForm(applicantParties);
    const user = await fillRequestFields();

    await user.selectOptions(screen.getByLabelText("Сохранённый заявитель"), "2");
    await user.click(screen.getByRole("button", { name: "Создать заявку" }));

    await waitFor(() => expect(onSuccess).toHaveBeenCalledTimes(1));
    expect(mockedCreateConnectionRequest).toHaveBeenCalledWith(
      expect.objectContaining({
        applicantContextType: "Existing",
        existingApplicantPartyId: 2,
      }),
    );
  });

  it("submits the New branch with applicant data", async () => {
    mockedCreateConnectionRequest.mockResolvedValue(undefined);
    const { onSuccess } = renderForm([]);
    const user = await fillRequestFields();

    await user.type(screen.getByLabelText("Имя"), "Ivan");
    await user.type(screen.getByLabelText("Отчество"), "Ivanovich");
    await user.type(screen.getByLabelText("Фамилия"), "Ivanov");
    await user.type(screen.getByLabelText("Email заявителя"), "ivan@example.com");
    await user.type(screen.getByLabelText("Телефон"), "+79001234567");
    await user.click(screen.getByRole("button", { name: "Создать заявку" }));

    await waitFor(() => expect(onSuccess).toHaveBeenCalledTimes(1));
    expect(mockedCreateConnectionRequest).toHaveBeenCalledWith(
      expect.objectContaining({
        applicantContextType: "New",
        existingApplicantPartyId: null,
        newApplicantParty: expect.objectContaining({
          email: "ivan@example.com",
          phoneNumber: "+79001234567",
        }),
      }),
    );
  }, 10_000);

  it("shows validation feedback for missing fields", async () => {
    renderForm(applicantParties);
    const user = userEvent.setup();

    await user.click(screen.getByRole("button", { name: "Создать заявку" }));

    expect(screen.getByText("Описание заявки: заполните поле.")).toBeVisible();
    expect(screen.getByText("Почтовый индекс: заполните поле.")).toBeVisible();
    expect(mockedCreateConnectionRequest).not.toHaveBeenCalled();
  });
});
