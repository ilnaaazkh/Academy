import { baseApi } from "../../shared/api";
import { Envelope } from "../../models/response/Envelope";

export type LoginResponse = {
  accessToken: string;
  refreshToken: string;
};

export const authApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    login: builder.mutation<
      Envelope<LoginResponse>,
      { email: string; password: string }
    >({
      query: ({ email, password }) => ({
        url: "/accounts/login",
        body: { email, password },
        method: "POST",
      }),
    }),
  }),
});

export const { useLoginMutation } = authApi;
