import { ServerRoutes } from "../globConstants";
import { fetchWrapper } from "../Utils/fetchWrapper";
import type { LoginDto } from "../Utils/FormSchemas";

export const login =async(dto: LoginDto): Promise<void>=>{
        const response = await fetchWrapper.post(
                                                ServerRoutes.Login.Path,
                                                dto,
                                                {credentials:'include'})

        if (response.ok) {
            return response.json();
        }else {
           throw await response.json();
        }
    }