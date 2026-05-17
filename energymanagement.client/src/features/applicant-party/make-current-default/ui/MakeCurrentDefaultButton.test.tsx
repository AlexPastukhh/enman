/**
 * @vitest environment jsdom
 */
import { cleanup, render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { afterEach, describe, expect, it, vi } from "vitest";
import { MakeCurrentDefaultButton } from "./MakeCurrentDefaultButton";

describe("MakeCurrentDefaultButton", () => {
  afterEach(() => {
    cleanup();
  });

  it("calls action when clicked", async () => {
    const user = userEvent.setup();
    const onClick = vi.fn();

    render(<MakeCurrentDefaultButton onClick={onClick} />);

    await user.click(screen.getByRole("button", { name: "Make current/default" }));

    expect(onClick).toHaveBeenCalledTimes(1);
  });

  it("shows pending and error states", () => {
    render(
      <MakeCurrentDefaultButton
        isPending
        errorMessage="Could not update default"
        onClick={vi.fn()}
      />,
    );

    expect(screen.getByRole("button", { name: "Making current/default..." })).toBeDisabled();
    expect(screen.getByRole("alert")).toHaveTextContent("Could not update default");
  });
});
