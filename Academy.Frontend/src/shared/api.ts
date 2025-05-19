import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { AppState } from "../store/store";
import { Envelope } from "../models/response/Envelope";
import { LoginResponse } from "../modules/auth/api";
import { setCredentials } from "../modules/auth/authSlice";

const BASE_URL = "https://localhost:7177/api";

const baseQuery = fetchBaseQuery({
  baseUrl: BASE_URL,
  credentials: "include",
  prepareHeaders: (headers, { getState }) => {
    const state = getState() as AppState;
    const token = state.auth.accessToken;

    if (token) {
      headers.set("authorization", `Bearer ${token}`);
    }
  },
});

export const baseQueryWithRefresh: typeof baseQuery = async (
  args,
  api,
  extraOptions
) => {
  let result = await baseQuery(args, api, extraOptions);

  if (result.error && result.error.status == 401) {
    const authResponse = await baseQuery(
      {
        url: "accounts/refresh",
        method: "POST",
      },
      api,
      extraOptions
    );

    if (authResponse.error) {
      return result;
    }

    const data = authResponse.data as Envelope<LoginResponse>;
    api.dispatch(
      setCredentials({
        accessToken: data.result!.accessToken,
        roles: data.result!.roles,
      })
    );
    result = await baseQuery(args, api, extraOptions);
  }

  return result;
};

export const baseApi = createApi({
  baseQuery: baseQueryWithRefresh,
  tagTypes: ["Courses", "Modules", "CourseInfo", "Lesson"],
  endpoints: () => ({}),
});
