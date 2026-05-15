import { zodResolver } from "@hookform/resolvers/zod";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import type { SubmitHandler } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import { applyApiErrorToForm } from "../../../../shared/api/applyApiErrorToForm";
import { clientRoutes } from "../../../../shared/config/clientRoutes";
import { useFormRegisterDebounce } from "../../../../shared/form/useFormRegisterDebounce";
import { useFormWrapper } from "../../../../shared/form/useFormWrapper";
import { sessionQueryKey } from "../../../../entities/session/model/sessionKeys";
import { loginClientAccount } from "../api/loginClientAccount";
import {
  loginFieldNames,
  loginSchema,
  loginServerFieldMap,
  type LoginFormValues,
} from "./loginSchema";

export const useLoginForm = () => {
  const {
    register: originalRegister,
    trigger,
    errors,
    isValid,
    isSubmitting,
    handleSubmit: originalHandleSubmit,
    setError,
  } = useFormWrapper<LoginFormValues>(zodResolver(loginSchema));

  const { register } = useFormRegisterDebounce(originalRegister, trigger);
  const navigate = useNavigate();
  const queryClient = useQueryClient();

  const loginMutation = useMutation({
    mutationFn: loginClientAccount,
    onError: (error) => {
      applyApiErrorToForm(error, setError, loginServerFieldMap);
    },
    onSuccess: async () => {
      await queryClient.invalidateQueries({ queryKey: sessionQueryKey });
      await navigate(clientRoutes.home);
    },
  });

  const onSubmit: SubmitHandler<LoginFormValues> = async (values) => {
    await loginMutation.mutateAsync(values);
  };

  return {
    register,
    errors,
    isValid,
    isSubmitting,
    loginFieldNames,
    handleSubmit: originalHandleSubmit(onSubmit),
  };
};

