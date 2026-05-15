/**
 * @vitest environment jsdom
 */
import { afterEach, describe, expect, it, vi } from "vitest";
import { getCurrentSession } from "./getCurrentSession";

afterEach(() => {
  vi.unstubAllGlobals();
});

describe("getCurrentSession", () => {
  it("maps current-user 401 to an unauthenticated session", async () => {
    vi.stubGlobal(
      "fetch",
      vi.fn().mockResolvedValue(
        new Response(null, {
          status: 401,
        }),
      ),
    );

    await expect(getCurrentSession()).resolves.toBeNull();
  });
});
