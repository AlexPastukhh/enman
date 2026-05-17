import { afterEach, describe, expect, it, vi } from "vitest";
import { makeApplicantPartyCurrentDefault } from "./makeApplicantPartyCurrentDefault";

const { postMakeApplicantPartyCurrentDefault } = vi.hoisted(() => ({
  postMakeApplicantPartyCurrentDefault: vi.fn(),
}));

vi.mock("../../../../shared/api/l1ApplicantPartyApi", () => ({
  makeApplicantPartyCurrentDefault: postMakeApplicantPartyCurrentDefault,
  __esModule: true,
}));

describe("makeApplicantPartyCurrentDefault", () => {
  afterEach(() => {
    postMakeApplicantPartyCurrentDefault.mockReset();
  });

  it("delegates to the shared API wrapper", async () => {
    postMakeApplicantPartyCurrentDefault.mockResolvedValue(undefined);

    await expect(makeApplicantPartyCurrentDefault(7)).resolves.toBeUndefined();

    expect(postMakeApplicantPartyCurrentDefault).toHaveBeenCalledWith(7);
  });
});
