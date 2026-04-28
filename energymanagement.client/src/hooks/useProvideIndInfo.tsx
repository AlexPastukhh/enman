import { useFormRegisterDebounce } from "./useFormRegisterDebounce"
import { useFormWrapper } from "./useFormWrapper"
import { FormSchemas, type LoginDto } from "../Utils/FormSchemas"
import { zodResolver } from "@hookform/resolvers/zod"
import type { SubmitHandler } from "react-hook-form"
import {  useMutation, useQueryClient } from "@tanstack/react-query"
import { login } from "../MutationFns/login"
import { isServerValidationError, setServerValidationErr } from "../Utils/ServerValidationErrUtils"
import { ClientRoutes, Keys } from "../globConstants"
import { useNavigate } from "react-router-dom"

export const useProvideIndInfo = ()=>{
    
    const {register:originalRegister,trigger,errors,isValid,isSubmitting,handleSubmit:originalHandleSubmit, setError}
        = useFormWrapper<LoginDto>(zodResolver(FormSchemas.Login.scheme))

    const {register} = useFormRegisterDebounce(originalRegister, trigger)

    const loginFieldNames=FormSchemas.Login.fieldNames;

    const navigate = useNavigate();

    const queryClient = useQueryClient(); 
    const loginMutation=useMutation({
        mutationFn:login,
        onError:(error)=>{
            if(isServerValidationError(error)){
                setServerValidationErr(error,setError)
            }   
        },
        onSuccess:async()=>{
            await navigate(ClientRoutes.Home.Path)
        },
        onSettled:()=>{
            queryClient.invalidateQueries({queryKey:[Keys.SessionQueryKey]})
        }
    })
    const onSubmit: SubmitHandler<LoginDto> = async (dto) => {
            loginMutation.mutate(dto)
    }
    const handleSubmit =originalHandleSubmit(onSubmit)

    return {register,errors,isValid,isSubmitting,loginFieldNames,handleSubmit}
}