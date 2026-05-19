import { describe, expect, it } from "vitest";
import { buildAgreementProposalDocumentDownloadPath } from "./buildAgreementProposalDocumentDownloadPath";

describe("buildAgreementProposalDocumentDownloadPath", () => {
  it("builds context-bound proposal document download URL", () => {
    expect(buildAgreementProposalDocumentDownloadPath(20, 200)).toBe(
      "/api/agreement-exchanges/20/proposals/200/document/download",
    );
  });
});
