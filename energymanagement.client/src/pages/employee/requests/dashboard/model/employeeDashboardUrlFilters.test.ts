import { describe, expect, it } from "vitest";
import {
  parseEmployeeDashboardUrlFilters,
  serializeEmployeeDashboardUrlFilters,
} from "./employeeDashboardUrlFilters";

describe("employeeDashboardUrlFilters", () => {
  it("parses supported status and review state filters", () => {
    const result = parseEmployeeDashboardUrlFilters(
      new URLSearchParams(
        "status=InReview&reviewState=StartedByCurrentEmployee",
      ),
    );

    expect(result).toEqual({
      filters: {
        status: "InReview",
        reviewState: "StartedByCurrentEmployee",
      },
      invalidFilterReason: null,
    });
  });

  it("rejects unknown status filters", () => {
    const result = parseEmployeeDashboardUrlFilters(
      new URLSearchParams("status=Unknown"),
    );

    expect(result.invalidFilterReason).toBe(
      "Unknown employee request status filter.",
    );
  });

  it("serializes filters", () => {
    const result = serializeEmployeeDashboardUrlFilters({
      status: "Approved",
      reviewState: "Rejected",
    });

    expect(result.toString()).toBe("status=Approved&reviewState=Rejected");
  });
});
