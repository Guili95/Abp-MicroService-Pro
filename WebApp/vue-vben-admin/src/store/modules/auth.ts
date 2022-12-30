import { defineStore } from 'pinia';

import { store } from '/@/store';
import { getAuthCache, setAuthCache } from '/@/utils/auth';
import Oidc, { UserManager } from 'oidc-client';
import { TOKEN_KEY } from '/@/enums/cacheEnum';

const host = location.origin;

const userManager: UserManager = new Oidc.UserManager({
  authority: 'https://auth.threebody.shop',
  client_id: 'ABPMicroServiceProAdmin',
  redirect_uri: encodeURI(`${host}/#/oidcSignIn`),
  response_type: 'code',
  scope:
    'AccountService IdentityService AdministrationService SaasService BackendAdminAppGateway openid',
});

interface OidcState {
  token: Nullable<TokenState>;
}

interface TokenState {
  access_token?: string;
  token?: string;
  expires_in?: number;
  scope?: string;
  token_type?: string;
}

export const useAuthStore = defineStore({
  id: 'app-auth',
  state: (): OidcState => ({
    token: null,
  }),
  getters: {
    getToken(): string {
      return this.token?.access_token || getAuthCache<string>(TOKEN_KEY) || '';
    },
  },
  actions: {
    async setToken(info: TokenState | null) {
      this.token = info;
      await setAuthCache(TOKEN_KEY, info?.access_token);
    },
    async signinRedirect() {
      await userManager.signinRedirect();
    },
    async signinRedirectCallback() {
      const token = await userManager.signinRedirectCallback();
      await this.setToken(token);
      return true;
    },
    async logout() {
      await userManager.signoutRedirect();
    },
  },
});

export function useAuthStoreWithOut() {
  return useAuthStore(store);
}
