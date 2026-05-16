import { describe, expect, it } from "vitest";
import {
  parseMyRequestsUrlFilters,
  serializeMyRequestsUrlFilters,
} from "./myRequestsUrlFilters";

describe("myRequestsUrlFilters", () => {
  it("parses empty filters", () => {
    expect(parseMyRequestsUrlFilters(new URLSearchParams())).toEqual({
      filters: {},
    });
  });

  it("parses a valid status filter", () => {
    expect(
      parseMyRequestsUrlFilters(new URLSearchParams("status=Approved")),
    ).toEqual({
      filters: { status: "Approved" },
    });
  });

  it("reports invalid status filter", () => {
    const parsed = parseMyRequestsUrlFilters(new URLSearchParams("status=Done"));

    expect(parsed.filters).toEqual({});
    expect(parsed.invalidFilterReason).toBe("Некорректный статус заявки.");
  });

  it("serializes filters to URLSearchParams", () => {
    expect(serializeMyRequestsUrlFilters({ status: "InReview" }).toString()).toBe(
      "status=InReview",
    );
  });
});
