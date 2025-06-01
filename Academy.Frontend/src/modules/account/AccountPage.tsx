import { Box } from "@mui/material";
import { AccountSidebar } from "./AccountSidebar";
import { Outlet } from "react-router";

export function AccountPage() {
  return (
    <Box display="flex" height="100vh" overflow="hidden">
      <AccountSidebar />
      <Box className="flex-1 overflow-y-auto px-4 py-4">
        <Outlet />
      </Box>
    </Box>
  );
}
