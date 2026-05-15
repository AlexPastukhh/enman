import { zodResolver } from "@hookform/resolvers/zod";
import { useMutation } from "@tanstack/react-query";
import { useEffect, useRef, useState } from "react";
import type { SubmitHandler } from "react-hook-form";
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

export const useCreateIndividualApplicantPartyForm = () => {
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
  const [savedApplicantParty, setSavedApplicantParty] =
    useState<CreateIndividualApplicantPartyFormValues | null>(null);
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
    onSuccess: (_data, values) => {
      setSavedApplicantParty(values);
      reset(values);
      showSuccessNotification();
    },
  });

  const onSubmit: SubmitHandler<CreateIndividualApplicantPartyFormValues> =
    async (values) => {
      await submitMutation.mutateAsync(values);
    };

  const startEditingSavedApplicantParty = () => {
    if (savedApplicantParty) {
      reset(savedApplicantParty);
    }

    setSavedApplicantParty(null);
    setIsSuccessNotificationVisible(false);
    clearNotificationTimeout();
  };

  return {
    register,
    handleSubmit: originalHandleSubmit(onSubmit),
    errors,
    isSubmitting,
    fieldNames: createIndividualApplicantPartyFieldNames,
    savedApplicantParty,
    isSuccessNotificationVisible,
    startEditingSavedApplicantParty,
  };
};
