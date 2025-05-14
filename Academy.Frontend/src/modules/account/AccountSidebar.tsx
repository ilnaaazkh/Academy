import { useSelector } from "react-redux";
import { Role, selectRoles } from "../auth/authSlice";
import {
  Box,
  List,
  ListItem,
  ListItemButton,
  ListItemText,
} from "@mui/material";
import { NavLink } from "react-router";

export function AccountSidebar() {
  const roles = useSelector(selectRoles);

  function isInRole(role: Role): boolean {
    return roles.includes(role);
  }

  return (
    <Box className="w-1/6 py-3 border-r h-screen overflow-y-auto">
      <List>
        {isInRole("Author") && (
          <ListItem>
            <ListItemButton
              key="own-courses"
              component={NavLink}
              to={`/profile/own-courses`}
            >
              <ListItemText primary={"Созданные курсы"} className="pl-2" />
            </ListItemButton>
          </ListItem>
        )}
      </List>
    </Box>
  );
}
