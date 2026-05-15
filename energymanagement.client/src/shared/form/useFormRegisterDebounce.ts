import { useRef } from "react";
import type { FieldValues, UseFormRegister, UseFormTrigger } from "react-hook-form";
import type { FormRegisterWithDebounce } from "./formTypes";

export const useFormRegisterDebounce = <TFormValues extends FieldValues>(
  registerFunc: UseFormRegister<TFormValues>,
  triggerFunc: UseFormTrigger<TFormValues>,
): FormRegisterWithDebounce<TFormValues> => {
  const timeOutRefs = useRef<Record<string, ReturnType<typeof setTimeout>>>({});

  const register: FormRegisterWithDebounce<TFormValues>["register"] = (name) => {
    const registerField = registerFunc(name);
    const fieldName = String(name);

    return {
      ...registerField,
      onChange: (event: React.ChangeEvent<HTMLInputElement>) => {
        registerField.onChange(event);
        if (timeOutRefs.current[fieldName]) {
          clearTimeout(timeOutRefs.current[fieldName]);
        }

        timeOutRefs.current[fieldName] = setTimeout(() => {
          void triggerFunc(name);
        }, 500);
      },
    };
  };

  return { register };
};

