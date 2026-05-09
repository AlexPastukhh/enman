import { act, fireEvent, type Screen } from "@testing-library/react";
import { formConst } from "../../../Components/Form/formConst";
import type { UserEvent } from "@testing-library/user-event";

export class BaseTest {}

const debouncedValidationDelayMs = 600;
const debouncedValidationPollIntervalMs = 50;

export class SUTFormField {
  private screen: Screen;

  private input: HTMLInputElement;
  private constructor(input: HTMLInputElement, screen: Screen) {
    this.input = input;
    this.screen = screen;
  }

  private static tryGetFormErrorOrNull = (
    screen: Screen,
    inputId: string
  ): HTMLParagraphElement | null => {
    const errFound = screen.queryByRole("alert", {
      name: formConst.getAriaLabelForError(inputId),
    });
    if (errFound) {
      if (!errFound.textContent) {
        return null;
      }
      return errFound as HTMLParagraphElement;
    }
    return null;
  };

  TryGetErrorMessageNow(): string | null {
    const errOrNull = SUTFormField.tryGetFormErrorOrNull(
      this.screen,
      this.input.id
    );
    return errOrNull?.textContent ?? null;
  }

  static Create = (screen: Screen, inputLabel: string) => {
    const input = screen.getByLabelText(inputLabel) as HTMLInputElement;
    return new SUTFormField(input, screen);
  };

  async FillAsync(user: UserEvent, value: string) {
    await user.type(this.input, value);
  }

  SetValue(value: string) {
    fireEvent.change(this.input, { target: { value } });
  }

  async WaitForDebouncedValidationAsync() {
    await act(async () => {
      await new Promise((resolve) =>
        setTimeout(resolve, debouncedValidationDelayMs)
      );
    });
  }

  async ExpectNoErrorDuringDebouncedValidationAsync() {
    const errorMessage = await this.TryGetErrorMessageDebounced();
    if (errorMessage) {
      throw new Error(
        `Expected no validation error during debounce, but found: ${errorMessage}`
      );
    }
  }

  HasErrorNow(): boolean {
    return this.TryGetErrorMessageNow() !== null;
  }

  async HasErrorDebounced(): Promise<boolean> {
    return (await this.TryGetErrorMessageDebounced()) !== null;
  }

  async GetErrorMessageDebounced(): Promise<string| Error> {
    const errOrNull = await this.TryGetErrorMessageDebounced();
    if (!errOrNull) {
      return new Error("No error present");
    }
    return errOrNull;
  }

  async TryGetErrorMessageDebounced(): Promise<string | null> {
    const startedAt = Date.now();

    while (Date.now() - startedAt < debouncedValidationDelayMs) {
      const errorMessage = this.TryGetErrorMessageNow();
      if (errorMessage) {
        return errorMessage;
      }

      await new Promise((resolve) =>
        setTimeout(resolve, debouncedValidationPollIntervalMs)
      );
    }

    return this.TryGetErrorMessageNow();
  }
}
