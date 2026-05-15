import type { FieldValues, UseFormRegister, UseFormTrigger } from "react-hook-form";

type RegisterReturn<TFormValues extends FieldValues> = Omit<
  ReturnType<UseFormRegister<TFormValues>>,
  "onChange"
> & {
  onChange: (event: React.ChangeEvent<HTMLInputElement>) => void;
};

export type DebouncedFormRegister<TFormValues extends FieldValues> = (
  name: Parameters<UseFormRegister<TFormValues>>[0],
) => RegisterReturn<TFormValues>;

export type FormRegisterWithDebounce<TFormValues extends FieldValues> = {
  register: DebouncedFormRegister<TFormValues>;
};

export type FormTrigger<TFormValues extends FieldValues> =
  UseFormTrigger<TFormValues>;

