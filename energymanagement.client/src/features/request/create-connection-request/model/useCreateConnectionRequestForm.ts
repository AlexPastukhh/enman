import { useMutation, useQueryClient } from "@tanstack/react-query";
import { useMemo, useState } from "react";
import type { ApplicantPartySummary } from "../../../../entities/applicant-party/model/applicantPartyTypes";
import { requestQueryKeys } from "../../../../entities/request/model/requestQueryKeys";
import { ApiError } from "../../../../shared/api/fetchJson";
import { problemDetailsToFormErrors } from "../../../../shared/api/problemDetails";
import { createConnectionRequest } from "../api/createConnectionRequest";
import { createConnectionRequestConst } from "../ui/createConnectionRequestConst";
import { createConnectionRequestServerFieldMap } from "./createConnectionRequestFieldMap";
import {
  buildCreateConnectionRequestDto,
  validateCreateConnectionRequestValues,
} from "./buildCreateConnectionRequestDto";
import {
  createConnectionRequestInitialValues,
  type ApplicantContextType,
  type CreateConnectionRequestFormErrors,
  type CreateConnectionRequestFormValues,
} from "./createConnectionRequestTypes";

type UseCreateConnectionRequestFormOptions = {
  applicantParties: ApplicantPartySummary[];
  onSuccess?: () => void;
};

const findInitialApplicantPartyId = (
  applicantParties: ApplicantPartySummary[],
) => {
  const currentDefault = applicantParties.find(
    (applicantParty) => applicantParty.isCurrentDefault,
  );
  const selected = currentDefault ?? applicantParties[0];
  return selected?.applicantPartyId ? String(selected.applicantPartyId) : "";
};

const apiErrorToFormErrors = (error: unknown): CreateConnectionRequestFormErrors => {
  if (error instanceof ApiError && error.problemDetails) {
    return Object.fromEntries(
      problemDetailsToFormErrors(
        error.problemDetails,
        createConnectionRequestServerFieldMap,
      ).map((formError) => [formError.fieldName, formError.message]),
    ) as CreateConnectionRequestFormErrors;
  }

  return {
    root: createConnectionRequestConst.rootErrorFallback,
  };
};

export const useCreateConnectionRequestForm = ({
  applicantParties,
  onSuccess,
}: UseCreateConnectionRequestFormOptions) => {
  
  const initialApplicantPartyId = useMemo(
    () => findInitialApplicantPartyId(applicantParties),
    [applicantParties],
  );
  const [values, setValues] = useState<CreateConnectionRequestFormValues>(() =>
    createConnectionRequestInitialValues(initialApplicantPartyId),
  );
  const [errors, setErrors] = useState<CreateConnectionRequestFormErrors>({});
  const queryClient = useQueryClient();

  const submitMutation = useMutation<void, unknown, CreateConnectionRequestFormValues>({
    mutationFn: (formValues) => createConnectionRequest(buildCreateConnectionRequestDto(formValues)),
    onError: (error) => {
      setErrors(apiErrorToFormErrors(error));
    },
    onSuccess: async () => {
      await queryClient.invalidateQueries({
        queryKey: requestQueryKeys.myRequests(),
      });
      onSuccess?.();
    },
  });

  const setFieldValue = (
    fieldName: keyof CreateConnectionRequestFormValues,
    value: string,
  ) => {
    setValues((currentValues) => ({
      ...currentValues,
      [fieldName]: value,
    }));
    setErrors((currentErrors) => ({
      ...currentErrors,
      [fieldName]: undefined,
      root: undefined,
    }));
  };

  const setApplicantContextType = (applicantContextType: ApplicantContextType) => {
    setValues((currentValues) => ({
      ...currentValues,
      applicantContextType,
      existingApplicantPartyId:
        applicantContextType === "Existing"
          ? currentValues.existingApplicantPartyId || initialApplicantPartyId
          : "",
    }));
    setErrors((currentErrors) => ({
      ...currentErrors,
      applicantContextType: undefined,
      existingApplicantPartyId: undefined,
      root: undefined,
    }));
  };

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const nextErrors = validateCreateConnectionRequestValues(values);
    setErrors(nextErrors);

    if (Object.keys(nextErrors).length > 0) {
      return;
    }

    await submitMutation.mutateAsync(values);
  };

  return {
    values,
    errors,
    isSubmitting: submitMutation.isPending,
    setFieldValue,
    setApplicantContextType,
    handleSubmit,
  };
};
