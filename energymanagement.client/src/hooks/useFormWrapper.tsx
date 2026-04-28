import { useForm, type Resolver } from "react-hook-form";
import type { TDto } from "../Utils/FormSchemas";

export const useFormWrapper = <T extends TDto>(resolver:Resolver<T>)=>{
    const {register,handleSubmit,formState:{errors,isSubmitting,isValid},setError, trigger} 
            = useForm<T>({
                resolver: resolver,
                mode: "onBlur"
        });
    return {register,
        handleSubmit,
        errors,
        isSubmitting,
        isValid,
        setError,
        trigger
    }
}