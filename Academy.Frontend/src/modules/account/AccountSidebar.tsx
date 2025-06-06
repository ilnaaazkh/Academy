import { useSelector } from "react-redux";
import { logout, Role, selectRoles } from "../auth/authSlice";
import {
  Box,
  List,
  ListItem,
  ListItemButton,
  ListItemIcon,
  ListItemText,
} from "@mui/material";
import { NavLink } from "react-router";
import { useAppDispatch } from "../../store/store";
import { Logout } from "@mui/icons-material";
import { baseApi } from "../../shared/api";

export function AccountSidebar() {
  const roles = useSelector(selectRoles);
  const dispatch = useAppDispatch();

  function isInRole(role: Role): boolean {
    return roles.includes(role);
  }

  return (
    <Box
      className="w-1/6 py-3 border-r h-screen overflow-y-auto"
      display="flex"
      flexDirection="column"
      justifyContent="space-between"
    >
      <div>
        <List>
          {isInRole("Student") && (
            <ListItem>
              <ListItemButton
                key="my-authorings"
                component={NavLink}
                to={`/profile/my-authorings`}
              >
                <ListItemText primary={"Стать автором"} className="pl-2" />
              </ListItemButton>
            </ListItem>
          )}
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
          {isInRole("Admin") && (
            <ListItem>
              <ListItemButton
                key="own-course"
                component={NavLink}
                to={`/profile/authors`}
              >
                <ListItemText primary={"Авторы"} className="pl-2" />
              </ListItemButton>
            </ListItem>
          )}
          {isInRole("Admin") && (
            <ListItem>
              <ListItemButton
                key="authrings"
                component={NavLink}
                to={`/profile/authorings`}
              >
                <ListItemText primary={"Заявки"} className="pl-2" />
              </ListItemButton>
            </ListItem>
          )}
          {isInRole("Admin") && (
            <ListItem>
              <ListItemButton
                key="publish"
                component={NavLink}
                to={`/profile/publish`}
              >
                <ListItemText
                  primary={"Запросы на публикацию"}
                  className="pl-2"
                />
              </ListItemButton>
            </ListItem>
          )}
        </List>
      </div>
      <Box>
        <List>
          <ListItem disablePadding>
            <ListItemButton
              onClick={() => {
                dispatch(logout());
                dispatch(baseApi.util.resetApiState());
              }}
              component={NavLink}
              to="/"
              sx={{ color: "error.main" }}
            >
              <ListItemIcon sx={{ color: "error.main" }}>
                <Logout />
              </ListItemIcon>
              <ListItemText primary="Выйти" className="pl-2" />
            </ListItemButton>
          </ListItem>
        </List>
      </Box>
    </Box>
  );
}
