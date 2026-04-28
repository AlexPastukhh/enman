import { waitFor, type Screen } from "@testing-library/react";
import { formConst } from "../../../Components/Form/formConst";
import type { UserEvent } from "@testing-library/user-event";
import { EMLogger, LogMsgs } from "../../../Utils/Logger";

export class BaseTest {}



class TimeoutErr extends Error {
  static TOMessage = "Timeout waiting for condition";
  constructor() {
    super(TimeoutErr.TOMessage);
    this.name = "TimeoutErr";
  }
}

export class SUTFormField {
  private screen: Screen;

  private input: HTMLInputElement;
  private error?: HTMLParagraphElement;
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
      return errFound as HTMLParagraphElement;
    }
    return null;
  };

  private static getErrorIfHasOne = async (
    screen: Screen,
    input: HTMLInputElement
  ): Promise<HTMLParagraphElement | null> => {
    const inputId = input.id;
    let error;
    try {
      error = await waitFor(
        () => {
          const errFound = this.tryGetFormErrorOrNull(screen, inputId);
          if (errFound) {
            EMLogger.Warn(LogMsgs.FormTests.FormErrorFound);
            return errFound;
          }
        },
        {
          timeout: 2000,
          interval: 100,
          onTimeout: () => {
            EMLogger.Warn(LogMsgs.FormTests.TimeoutWaitingForCondition);
            throw new TimeoutErr();
          },
        }
      );
    } catch (err: unknown) {
      if (err instanceof TimeoutErr) {
        return null;
      }
    }
    return error as HTMLParagraphElement | null;
  };
  static Create = (screen: Screen, inputLabel: string) => {
    // const nameRegex = new RegExp(`^${inputLabel}$`);
    const input = screen.getByRole("textbox", {
      name: inputLabel,
    }) as HTMLInputElement;
    return new SUTFormField(input, screen);
  };

  async FillAsync(user: UserEvent, value: string) {
    await user.type(this.input, value);
  }


  async HasError(): Promise<boolean> {
    const errOrNull = await SUTFormField.getErrorIfHasOne(
      this.screen,
      this.input
    );
    if (errOrNull) {
      return true;
    }
    return false;
  }
  async GetErrorMessage(): Promise<string| Error> {
    const errOrNull = await SUTFormField.getErrorIfHasOne(
      this.screen,
      this.input
    );
    if(!errOrNull){
      return new Error("No error present");
    }
    return errOrNull.textContent;
  }
async TryGetErrorMessage(): Promise<string| null> {
    const errOrNull = await SUTFormField.getErrorIfHasOne(
      this.screen,
      this.input
    );
    if(!errOrNull){
      return null;
    }
    return errOrNull.textContent;
  }
}
