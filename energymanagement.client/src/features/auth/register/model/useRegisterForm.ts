import { zodResolver } from "@hookform/resolvers/zod";
import { useMutation } from "@tanstack/react-query";
import type { SubmitHandler } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import { applyApiErrorToForm } from "../../../../shared/api/applyApiErrorToForm";
import { clientRoutes } from "../../../../shared/config/clientRoutes";
import { useFormRegisterDebounce } from "../../../../shared/form/useFormRegisterDebounce";
import { useFormWrapper } from "../../../../shared/form/useFormWrapper";
import { registerClientAccount } from "../api/registerClientAccount";
import {
  registerFieldNames,
  registerSchema,
  registerServerFieldMap,
  type RegisterFormValues,
} from "./registerSchema";

export const useRegisterForm = () => {
  const {
    errors,
    isSubmitting,
    isValid,
    setError,
    trigger,
    register: originalRegister,
    handleSubmit: originalHandleSubmit,
  } = useFormWrapper<RegisterFormValues>(zodResolver(registerSchema));

  const { register } = useFormRegisterDebounce(originalRegister, trigger);
  const navigate = useNavigate();

  const submitMutation = useMutation<void, unknown, RegisterFormValues>({
    mutationFn: registerClientAccount,
    onError: (error) => {
      applyApiErrorToForm(error, setError, registerServerFieldMap);
    },
    onSuccess: async () => {
      await navigate(clientRoutes.login);
    },
  });

  const onSubmit: SubmitHandler<RegisterFormValues> = async (values) => {
    await submitMutation.mutateAsync(values);
  };

  return {
    register,
    handleSubmit: originalHandleSubmit(onSubmit),
    errors,
    isSubmitting,
    isValid,
    fieldNames: registerFieldNames,
  };
};

