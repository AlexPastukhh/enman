import { useForm, type FieldValues, type Resolver } from "react-hook-form";

export const useFormWrapper = <TFormValues extends FieldValues>(
  resolver: Resolver<TFormValues>,
) => {
  const {
    register,
    handleSubmit,
    formState: { errors, isSubmitting, isValid },
    setError,
    trigger,
    reset,
  } = useForm<TFormValues>({
    resolver,
    mode: "onBlur",
  });

  return {
    register,
    handleSubmit,
    errors,
    isSubmitting,
    isValid,
    setError,
    trigger,
    reset,
  };
};
