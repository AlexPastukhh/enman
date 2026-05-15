export function escapeRegExp(value: string) {
  return value.replace(/[.*+?^${}()|[\]\\]/g, "\\$&");
}

export function exactTextIgnoreCase(value: string) {
  return new RegExp(`^${escapeRegExp(value)}$`, "i");
}
