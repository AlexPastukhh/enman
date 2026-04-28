import { ServerRoutes } from "../globConstants";
import { fetchWrapper } from "../Utils/fetchWrapper";
import type { RegisterDto } from "../Utils/FormSchemas";

export const registerIndClient = async (
  dto: RegisterDto
): Promise<Response> => {
  const response = await fetchWrapper.post(
    ServerRoutes.RegisterIndividual.Path,
    dto
  );

  if (response.ok) {
    return response.json();
  } else {
    const data = await response.json();
    return Promise.reject(data);

  }
};
