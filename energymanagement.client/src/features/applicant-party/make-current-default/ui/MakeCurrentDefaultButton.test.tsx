/**
 * @vitest environment jsdom
 */
import { cleanup, render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { afterEach, describe, expect, it, vi } from "vitest";
import { MakeCurrentDefaultButton } from "./MakeCurrentDefaultButton";

describe("MakeCurrentDefaultButton", () => {
  afterEach(() => {
    cleanup();
  });

  it("calls action when clicked", async () => {
    const user = userEvent.setup();
    const onClick = vi.fn();

    render(<MakeCurrentDefaultButton onClick={onClick} />);

    await user.click(screen.getByRole("button", { name: "Сделать текущим" }));

    expect(onClick).toHaveBeenCalledTimes(1);
  });

  it("shows pending and error states", () => {
    render(
      <MakeCurrentDefaultButton
        isPending
        errorMessage="Не удалось обновить текущего заявителя."
        onClick={vi.fn()}
      />,
    );

    expect(screen.getByRole("button", { name: "Назначаем текущим..." })).toBeDisabled();
    expect(screen.getByRole("alert")).toHaveTextContent(
      "Не удалось обновить текущего заявителя.",
    );
  });
});
