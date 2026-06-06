import { zodResolver } from "@hookform/resolvers/zod";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import type { SubmitHandler } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import { sessionQueryKey } from "../../../../entities/session/model/sessionKeys";
import { applyApiErrorToForm } from "../../../../shared/api/applyApiErrorToForm";
import { clientRoutes } from "../../../../shared/config/clientRoutes";
import { useFormRegisterDebounce } from "../../../../shared/form/useFormRegisterDebounce";
import { useFormWrapper } from "../../../../shared/form/useFormWrapper";
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
      if (error instanceof Error && "problemDetails" in error) {
        applyApiErrorToForm(error, setError, loginServerFieldMap);
        return;
      }

      setError("root" as const, {
        type: "server",
        message:
          "\u041d\u0435 \u0443\u0434\u0430\u043b\u043e\u0441\u044c \u0432\u043e\u0439\u0442\u0438. \u041f\u0440\u043e\u0432\u0435\u0440\u044c\u0442\u0435 email \u0438 \u043f\u0430\u0440\u043e\u043b\u044c.",
      });
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
