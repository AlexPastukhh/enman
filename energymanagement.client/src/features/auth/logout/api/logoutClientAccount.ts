import { logoutClientAccount as postLogoutClientAccount } from "../../../../shared/api/l1AuthApi";

export const logoutClientAccount = (): Promise<void> => postLogoutClientAccount();

