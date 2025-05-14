import { baseApi } from "../../shared/api";
import { Envelope } from "../../models/response/Envelope";
import { Role } from "./authSlice";

export type LoginResponse = {
  accessToken: string;
  refreshToken: string;
  roles: Role[];
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
    registration: builder.mutation<
      Envelope<void>,
      { email: string; password: string }
    >({
      query: ({ email, password }) => ({
        url: "/accounts/register",
        body: { email, password },
        method: "POST",
      }),
    }),
    refresh: builder.mutation<Envelope<LoginResponse>, void>({
      query: () => ({
        url: "/accounts/refresh",
        method: "POST",
      }),
    }),
  }),
});

export const { useLoginMutation, useRegistrationMutation, useRefreshMutation } =
  authApi;
