import { createSlice, PayloadAction } from "@reduxjs/toolkit/react";

export type Role = "Admin" | "Author" | "Student";

export type AuthState = {
  accessToken: string | undefined;
  isAuthenticated: boolean;
  fetchingStatus: "idle" | "loading" | "succeeded" | "failed";
  roles: Role[];
};

const initialAuthState: AuthState = {
  accessToken: undefined,
  isAuthenticated: false,
  fetchingStatus: "idle",
  roles: [],
};

export const authSlice = createSlice({
  name: "auth",
  initialState: initialAuthState,
  selectors: {
    selectAccessToken: (state) => state.accessToken,
    selectIsAuthenticated: (state) => state.isAuthenticated,
    selectRoles: (state) => state.roles,
    selectFetchingStatus: (state) => state.fetchingStatus,
  },
  reducers: {
    setCredentials: (
      state,
      action: PayloadAction<{ accessToken: string; roles: string[] }>
    ) => {
      state.accessToken = action.payload.accessToken;
      state.isAuthenticated = true;
      state.fetchingStatus = "succeeded";
      state.roles = action.payload.roles;
    },
    logout: (state) => {
      state.accessToken = undefined;
      state.fetchingStatus = "idle";
      state.isAuthenticated = false;
      state.roles = [];
    },
  },
});

export const { setCredentials, logout } = authSlice.actions;
export const {
  selectAccessToken,
  selectIsAuthenticated,
  selectFetchingStatus,
  selectRoles,
} = authSlice.selectors;
export default authSlice.reducer;
