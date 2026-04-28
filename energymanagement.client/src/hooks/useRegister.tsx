import { type SubmitHandler } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";

import { FormSchemas, type RegisterDto } from "../Utils/FormSchemas";
import { useMutation } from "@tanstack/react-query";
import { useFormRegisterDebounce } from "./useFormRegisterDebounce";
import { registerIndClient } from "../MutationFns/registerIndClient";
import { useFormWrapper } from "./useFormWrapper";
import { useNavigate } from "react-router-dom";
import { ClientRoutes } from "../globConstants";
import { handleErrorResponse } from "../Utils/handleErrorResponse";

export type RegisterType = ReturnType<typeof useRegister>["register"];

export const useRegister = () => {
  const { scheme: registerScheme, fieldNames } = FormSchemas.Register;
  const {
    errors,
    isSubmitting,
    isValid,
    setError,
    trigger,
    register: originalRegister,
    handleSubmit: originalHandleSubmit,
  } = useFormWrapper<RegisterDto>(zodResolver(registerScheme));

  const { register } = useFormRegisterDebounce(originalRegister, trigger);

  const navigate = useNavigate();
  const submitMutation = useMutation<Response, Response, RegisterDto>({
    mutationFn: registerIndClient,
    onError: (response) => {
      handleErrorResponse(response, setError);
    },
    onSuccess: async () => {
      await navigate(ClientRoutes.Login.Path);
    },
  });

  const onSubmit: SubmitHandler<RegisterDto> = async (dto) => {
    await submitMutation.mutate(dto);
  };
  const handleSubmit = originalHandleSubmit(onSubmit);

  return { register, handleSubmit, errors, isSubmitting, isValid, fieldNames };
};
