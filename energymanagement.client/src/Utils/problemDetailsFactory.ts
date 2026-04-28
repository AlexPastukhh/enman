import { generalConstants } from "../globConstants";
import { isServerValidationErrorArray, type ServerValidationError } from "./ServerValidationErrUtils";

export interface ProblemDetails {
  type?: string;
  title?: string;
  status?: number;
  detail?: string;
  instance?: string;
  [key: string]: unknown; // extensions
}

export const isProblemDetails = (error:unknown):error is ProblemDetails=>{
  return (typeof error ==="object" 
                && error !== null
                && "type" in error
                && "title" in error
                && "status" in error
                && "detail" in error
                && "instance" in error);
}

export const getServerErrorsIfAny=(problemDetails: ProblemDetails):ServerValidationError[]|undefined=>{
  if(generalConstants.ErrorsCollectionName in problemDetails){
    const errors = problemDetails[generalConstants.ErrorsCollectionName];
    if(isServerValidationErrorArray(errors)){
      return errors
    }
  }
  return undefined
}



