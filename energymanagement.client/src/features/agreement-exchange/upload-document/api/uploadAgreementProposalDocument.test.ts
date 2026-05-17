import { afterEach, describe, expect, it, vi } from "vitest";
import { clearAntiforgeryToken } from "../../../../shared/api/antiforgeryTokenStore";
import { uploadAgreementProposalDocument } from "./uploadAgreementProposalDocument";

afterEach(() => {
  clearAntiforgeryToken();
  vi.unstubAllGlobals();
});

describe("uploadAgreementProposalDocument", () => {
  it("posts selected file as multipart form data", async () => {
    const responseBody = {
      storageKey: "agreement-proposals/file.pdf",
      originalFileName: "file.pdf",
      contentType: "application/pdf",
      sizeBytes: 7,
    };
    const fetchMock = vi
      .fn()
      .mockResolvedValueOnce(tokenResponse("token-1"))
      .mockResolvedValueOnce(
        new Response(JSON.stringify(responseBody), {
          status: 200,
          headers: { "content-type": "application/json" },
        }),
      );
    vi.stubGlobal("fetch", fetchMock);

    const file = new File(["content"], "file.pdf", { type: "application/pdf" });

    await expect(uploadAgreementProposalDocument({ document: file })).resolves.toEqual(
      responseBody,
    );

    expect(fetchMock.mock.calls[1]?.[0]).toBe("/api/agreement-proposal-documents");
    const request = fetchMock.mock.calls[1]?.[1] as RequestInit;
    expect(request.method).toBe("POST");
    expect(request.body).toBeInstanceOf(FormData);

    const formData = request.body as FormData;
    expect(formData.get("document")).toBe(file);
  });
});

const tokenResponse = (requestToken: string) =>
  new Response(JSON.stringify({ requestToken }), {
    status: 200,
    headers: { "content-type": "application/json" },
  });
