import { ServerRoutes } from "../globConstants";
import { fetchWrapper } from "../Utils/fetchWrapper";
import type { RegisterDto } from "../Utils/FormSchemas";

export const registerIndClient = async (
  dto: RegisterDto
):Promise<void> => {
  const response = await fetchWrapper.post(
    ServerRoutes.RegisterIndividual.Path,
    dto
  );

  if (response.ok) {
    return;
  } else {
    throw await response.json();
  }
};
