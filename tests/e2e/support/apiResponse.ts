import type { Page, Response } from "@playwright/test";

export function waitForApiResponse(
  page: Page,
  method: string,
  pathPart: string
): Promise<Response> {
  return page.waitForResponse(
    (response) =>
      response.url().includes(pathPart) &&
      response.request().method() === method
  );
}
