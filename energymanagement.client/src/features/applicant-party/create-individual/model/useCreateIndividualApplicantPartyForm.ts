import { zodResolver } from "@hookform/resolvers/zod";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { useEffect, useRef, useState } from "react";
import type { SubmitHandler } from "react-hook-form";
import { applicantPartyQueryKeys } from "../../../../entities/applicant-party/model/applicantPartyQueryKeys";
import { applyApiErrorToForm } from "../../../../shared/api/applyApiErrorToForm";
import { useFormRegisterDebounce } from "../../../../shared/form/useFormRegisterDebounce";
import { useFormWrapper } from "../../../../shared/form/useFormWrapper";
import { createIndividualApplicantParty } from "../api/createIndividualApplicantParty";
import {
  createIndividualApplicantPartyFieldNames,
  createIndividualApplicantPartySchema,
  createIndividualApplicantPartyServerFieldMap,
  type CreateIndividualApplicantPartyFormValues,
} from "./createIndividualApplicantPartySchema";

const successNotificationTimeoutMs = 3_500;

type UseCreateIndividualApplicantPartyFormOptions = {
  onSuccess?: () => void;
};

export const useCreateIndividualApplicantPartyForm = (
  options: UseCreateIndividualApplicantPartyFormOptions = {},
) => {
  const {
    errors,
    isSubmitting,
    setError,
    trigger,
    register: originalRegister,
    handleSubmit: originalHandleSubmit,
    reset,
  } = useFormWrapper<CreateIndividualApplicantPartyFormValues>(
    zodResolver(createIndividualApplicantPartySchema),
  );

  const { register } = useFormRegisterDebounce(originalRegister, trigger);
  const queryClient = useQueryClient();
  const [isSuccessNotificationVisible, setIsSuccessNotificationVisible] =
    useState(false);
  const notificationTimeoutRef = useRef<ReturnType<typeof setTimeout> | null>(
    null,
  );

  const clearNotificationTimeout = () => {
    if (notificationTimeoutRef.current) {
      clearTimeout(notificationTimeoutRef.current);
      notificationTimeoutRef.current = null;
    }
  };

  const showSuccessNotification = () => {
    clearNotificationTimeout();
    setIsSuccessNotificationVisible(true);
    notificationTimeoutRef.current = setTimeout(() => {
      setIsSuccessNotificationVisible(false);
      notificationTimeoutRef.current = null;
    }, successNotificationTimeoutMs);
  };

  useEffect(() => clearNotificationTimeout, []);

  const submitMutation = useMutation<
    void,
    unknown,
    CreateIndividualApplicantPartyFormValues
  >({
    mutationFn: createIndividualApplicantParty,
    onError: (error) => {
      applyApiErrorToForm(
        error,
        setError,
        createIndividualApplicantPartyServerFieldMap,
      );
    },
    onSuccess: async (_data, values) => {
      await queryClient.invalidateQueries({
        queryKey: applicantPartyQueryKeys.currentIndividual,
      });
      reset(values);
      showSuccessNotification();
      options.onSuccess?.();
    },
  });

  const onSubmit: SubmitHandler<CreateIndividualApplicantPartyFormValues> =
    async (values) => {
      await submitMutation.mutateAsync(values);
    };

  return {
    register,
    handleSubmit: originalHandleSubmit(onSubmit),
    errors,
    isSubmitting,
    fieldNames: createIndividualApplicantPartyFieldNames,
    isSuccessNotificationVisible,
  };
};
