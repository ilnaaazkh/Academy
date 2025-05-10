import {
  Box,
  Divider,
  List,
  ListItemButton,
  ListItemText,
  Typography,
} from "@mui/material";
import { LessonInfoDto, ModuleDto } from "../models/moduleDto";
import { NavLink } from "react-router-dom";

export interface Props {
  modules: ModuleDto[];
  id: string;
}

export default function CourseSidebar({ modules, id }: Props) {
  return (
    <Box className="w-1/6 m-0 p-4 border-r h-screen overflow-y-auto">
      {modules.map((module: ModuleDto) => (
        <Box key={module.id} mb={2}>
          <Typography variant="h6" className="text-start">
            {module.title}
          </Typography>
          <List dense>
            {module.lessons.map((lesson: LessonInfoDto) => (
              <ListItemButton
                key={lesson.id}
                component={NavLink}
                to={`/${id}/lessons/${lesson.id}`}
              >
                <ListItemText primary={lesson.title} />
              </ListItemButton>
            ))}
          </List>
          <Divider />
        </Box>
      ))}
    </Box>
  );
}
