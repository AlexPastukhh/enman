import { beforeEach, describe, expect, it, vi } from "vitest";
import { listEmployeeRequests } from "../../../shared/api/employeeRequestApi";
import { listEmployeeDashboardRequests } from "./listEmployeeDashboardRequests";

vi.mock("../../../shared/api/employeeRequestApi", () => ({
  listEmployeeRequests: vi.fn(),
}));

const listEmployeeRequestsMock = vi.mocked(listEmployeeRequests);

describe("listEmployeeDashboardRequests", () => {
  beforeEach(() => {
    listEmployeeRequestsMock.mockReset();
  });

  it("delegates dashboard filters to the shared API wrapper", async () => {
    const response = { requests: [] };
    listEmployeeRequestsMock.mockResolvedValueOnce(response);

    await expect(
      listEmployeeDashboardRequests({
        status: "InReview",
        reviewState: "NotStarted",
      }),
    ).resolves.toBe(response);

    expect(listEmployeeRequestsMock).toHaveBeenCalledWith({
      status: "InReview",
      reviewState: "NotStarted",
    });
  });
});
