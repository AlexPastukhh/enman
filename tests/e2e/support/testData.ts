export const validPassword = "ValidPassword111!";

export function uniqueEmail(prefix = "e2e") {
  return `${prefix}.${Date.now()}.${Math.random().toString(36).slice(2)}@example.com`;
}
