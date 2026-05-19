import { logoutClientAccount as postLogoutClientAccount } from "../../../../shared/apiAuthApi";

export const logoutClientAccount = (): Promise<void> => postLogoutClientAccount();

