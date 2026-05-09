import { fireEvent, type Screen } from "@testing-library/react";
import { formConst } from "../../../Components/Form/formConst";
import type { UserEvent } from "@testing-library/user-event";

export class BaseTest {}

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

  HasErrorNow(): boolean {
    return this.TryGetErrorMessageNow() !== null;
  }
}
