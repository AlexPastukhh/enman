import { zodResolver } from "@hookform/resolvers/zod";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { useEffect, useRef, useState } from "react";
import { useForm, type FieldValues, type SubmitHandler } from "react-hook-form";
import { applicantPartyQueryKeys } from "../../../../entities/applicant-party/model/applicantPartyQueryKeys";
import { applyApiErrorToForm } from "../../../../shared/api/applyApiErrorToForm";
import type { DebouncedFormRegister } from "../../../../shared/form/formTypes";
import { useFormRegisterDebounce } from "../../../../shared/form/useFormRegisterDebounce";
import { createApplicantParty } from "../api/createApplicantParty";
import {
  createApplicantPartyFieldNames,
  createApplicantPartySchema,
  createApplicantPartyServerFieldMap,
  type CreateApplicantPartyFormValues,
} from "./createApplicantPartySchema";

const successNotificationTimeoutMs = 3_500;

const defaultValues: CreateApplicantPartyFormValues = {
  applicantPartyType: "Individual",
  firstName: "",
  middleName: "",
  lastName: "",
  organizationName: "",
  inn: "",
  kpp: "",
  ogrn: "",
  ogrnip: "",
  email: "",
  phoneNumber: "",
};

type UseCreateApplicantPartyFormOptions = {
  onSuccess?: () => void;
};

export const useCreateApplicantPartyForm = (
  options: UseCreateApplicantPartyFormOptions = {},
) => {
  const {
    register: originalRegister,
    handleSubmit: originalHandleSubmit,
    formState: { errors, isSubmitting },
    setError,
    trigger,
    reset,
    watch,
  } = useForm<CreateApplicantPartyFormValues>({
    resolver: zodResolver(createApplicantPartySchema),
    mode: "onBlur",
    defaultValues,
  });

  const { register } = useFormRegisterDebounce(originalRegister, trigger);
  const queryClient = useQueryClient();
  const [isSuccessNotificationVisible, setIsSuccessNotificationVisible] =
    useState(false);
  const notificationTimeoutRef = useRef<ReturnType<typeof setTimeout> | null>(
    null,
  );

  const applicantPartyType = watch(
    createApplicantPartyFieldNames.applicantPartyType,
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
    CreateApplicantPartyFormValues
  >({
    mutationFn: createApplicantParty,
    onError: (error) => {
      applyApiErrorToForm(error, setError, createApplicantPartyServerFieldMap);
    },
    onSuccess: async (_data, values) => {
      await queryClient.invalidateQueries({
        queryKey: applicantPartyQueryKeys.currentIndividual,
      });
      reset({
        ...defaultValues,
        applicantPartyType: values.applicantPartyType,
      });
      showSuccessNotification();
      options.onSuccess?.();
    },
  });

  const onSubmit: SubmitHandler<CreateApplicantPartyFormValues> = async (
    values,
  ) => {
    await submitMutation.mutateAsync(values);
  };

  return {
    register: register as unknown as DebouncedFormRegister<FieldValues>,
    registerRaw: originalRegister,
    handleSubmit: originalHandleSubmit(onSubmit),
    errors,
    isSubmitting,
    applicantPartyType,
    fieldNames: createApplicantPartyFieldNames,
    isSuccessNotificationVisible,
  };
};
