import { createSlice, PayloadAction } from "@reduxjs/toolkit/react";

export type AuthState = {
  accessToken: string | undefined;
  isAuthenticated: boolean;
  fetchingStatus: "idle" | "loading" | "succeeded" | "failed";
};

const initialAuthState: AuthState = {
  accessToken: undefined,
  isAuthenticated: false,
  fetchingStatus: "idle",
};

export const authSlice = createSlice({
  name: "auth",
  initialState: initialAuthState,
  selectors: {
    selectAccessToken: (state) => state.accessToken,
    selectIsAuthenticated: (state) => state.isAuthenticated,
  },
  reducers: {
    setCredentials: (state, action: PayloadAction<{ accessToken: string }>) => {
      state.accessToken = action.payload.accessToken;
      state.isAuthenticated = true;
      state.fetchingStatus = "succeeded";
    },
    logout: (state) => {
      state.accessToken = undefined;
      state.fetchingStatus = "idle";
      state.isAuthenticated = false;
    },
  },
});

export const { setCredentials, logout } = authSlice.actions;
export const { selectAccessToken, selectIsAuthenticated } = authSlice.selectors;
export default authSlice.reducer;
