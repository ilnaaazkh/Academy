import {
  Box,
  Divider,
  List,
  ListItemButton,
  ListItemText,
  Typography,
} from "@mui/material";
import { LessonInfoDto, ModuleDto } from "../../../courses/models/moduleDto";
import { NavLink } from "react-router-dom";
import { ArrowBack } from "@mui/icons-material";

export interface Props {
  modules: ModuleDto[];
  id: string;
}

export default function ModerateCourseSidebar({ modules, id }: Props) {
  const linkToMain = `/moderate/${id}`;
  return (
    <Box className="w-1/6 py-3 border-r h-screen overflow-y-auto">
      <ListItemButton
        component={NavLink}
        to="/profile/publish"
        sx={{ pl: 2, mb: 2 }}
      >
        <ArrowBack fontSize="small" sx={{ mr: 1 }} />
        <ListItemText primary="К списку курсов" />
      </ListItemButton>
      <ListItemButton component={NavLink} to={linkToMain} sx={{ pl: 2, mb: 2 }}>
        <ListItemText primary="Главная" />
      </ListItemButton>
      {modules.map((module: ModuleDto) => (
        <Box key={module.id} mb={2}>
          <Typography variant="h6" className="text-start pl-3">
            {module.title}
          </Typography>
          <List dense>
            {module.lessons.map((lesson: LessonInfoDto) => (
              <ListItemButton
                key={lesson.id}
                component={NavLink}
                to={`lessons/${lesson.id}`}
              >
                <ListItemText primary={lesson.title} className="pl-2" />
              </ListItemButton>
            ))}
          </List>
          <Divider />
        </Box>
      ))}
    </Box>
  );
}
