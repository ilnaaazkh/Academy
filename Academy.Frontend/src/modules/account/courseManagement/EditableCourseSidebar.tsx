import {
  Box,
  Divider,
  List,
  ListItemButton,
  ListItemText,
  Typography,
  IconButton,
  ListItem,
  ListItemIcon,
  Button,
} from "@mui/material";
import { LessonInfoDto, ModuleDto } from "../../courses/models/moduleDto";
import { NavLink } from "react-router-dom";
import { Edit, Delete, Add, ArrowBack, FastForward } from "@mui/icons-material";
import { useState } from "react";
import AddModuleModal from "./AddModuleModal";
import DeleteModuleModal from "./DeleteModuleModal";
import EditModuleModal from "./EditModuleModal";
import AddLessonModal from "./AddLessonModal";
import DeleteLessonModal from "./DeleteLessonModal";

export interface Props {
  modules: ModuleDto[];
  id: string;
}

export default function EditableCourseSidebar({ modules, id }: Props) {
  const [isAddModuleOpen, setIsAddModuleOpen] = useState(false);
  const [isDeleteModuleOpen, setIsDeleteModuleOpen] = useState(false);
  const [isUpdateModuleOpen, setIsUpdateModuleOpen] = useState(false);
  const [isAddLessonOpen, setIsAddLessonOpen] = useState(false);
  const [isDeleteLessonOpen, setIsDeleteLessonOpen] = useState(false);

  const [selectedModule, setSelectedModule] = useState<{
    title: string;
    id: string;
  }>({ title: "", id: "" });

  const [selectedLesson, setSelectedLesson] = useState<{
    title: string;
    id: string;
  }>({ title: "", id: "" });

  function handleDeleteModuleClick(moduleId: string, title: string) {
    setSelectedModule({ id: moduleId, title });
    setIsDeleteModuleOpen(true);
  }

  function handleUpdateModuleClick(moduleId: string, title: string) {
    setSelectedModule({ id: moduleId, title });
    setIsUpdateModuleOpen(true);
  }

  function handleAddLessonClick(moduleId: string, title: string) {
    setSelectedModule({ id: moduleId, title });
    setIsAddLessonOpen(true);
  }

  function handleDeleteLessonClick(
    moduleId: string,
    title: string,
    lessonId: string,
    lessonTitle: string
  ) {
    setSelectedModule({ id: moduleId, title });
    setSelectedLesson({ id: lessonId, title: lessonTitle });
    setIsDeleteLessonOpen(true);
  }

  const linkToMain = `/own-courses/${id}`;

  return (
    <>
      <Box className="w-1/6 py-3 border-r h-screen overflow-y-auto">
        <ListItemButton
          component={NavLink}
          to="/profile/own-courses"
          sx={{ pl: 2, mb: 2 }}
        >
          <ArrowBack fontSize="small" sx={{ mr: 1 }} />
          <ListItemText primary="К списку курсов" />
        </ListItemButton>
        <ListItemButton
          component={NavLink}
          to={linkToMain}
          sx={{ pl: 2, mb: 2 }}
        >
          <ListItemText primary="Главная" />
        </ListItemButton>

        {modules.map((module: ModuleDto) => (
          <Box key={module.id} mb={2}>
            <Box display="flex" alignItems="center">
              <Typography variant="h6" className="text-start pl-3 flex-grow">
                {module.title}
              </Typography>
              <IconButton
                onClick={() => handleUpdateModuleClick(module.id, module.title)}
              >
                <Edit fontSize="small" />
              </IconButton>
              <IconButton
                onClick={() => handleDeleteModuleClick(module.id, module.title)}
              >
                <Delete fontSize="small" />
              </IconButton>
            </Box>

            <List dense>
              {module.lessons.map((lesson: LessonInfoDto) => (
                <ListItem key={lesson.id} disablePadding>
                  <ListItemButton
                    component={NavLink}
                    to={`/${id}/lessons/${lesson.id}`}
                  >
                    <ListItemText primary={lesson.title} className="pl-2" />
                  </ListItemButton>
                  <ListItemIcon>
                    <IconButton onClick={() => {}}>
                      <Edit fontSize="small" />
                    </IconButton>
                    <IconButton
                      onClick={() => {
                        handleDeleteLessonClick(
                          module.id,
                          module.title,
                          lesson.id,
                          lesson.title
                        );
                      }}
                    >
                      <Delete fontSize="small" />
                    </IconButton>
                  </ListItemIcon>
                </ListItem>
              ))}

              <ListItem>
                <Button
                  onClick={() => handleAddLessonClick(module.id, module.title)}
                  startIcon={<Add />}
                  fullWidth
                >
                  Добавить урок
                </Button>
              </ListItem>
            </List>
            <Divider />
          </Box>
        ))}

        <Box p={2}>
          <Button
            variant="contained"
            startIcon={<Add />}
            fullWidth
            onClick={() => setIsAddModuleOpen(true)}
          >
            Добавить модуль
          </Button>
        </Box>
      </Box>
      <AddModuleModal
        isOpen={isAddModuleOpen}
        courseId={id}
        onClose={() => {
          setIsAddModuleOpen(false);
        }}
      />
      <DeleteModuleModal
        isOpen={isDeleteModuleOpen}
        onClose={() => setIsDeleteModuleOpen(false)}
        courseId={id}
        moduleId={selectedModule.id}
      />
      <EditModuleModal
        isOpen={isUpdateModuleOpen}
        onClose={() => setIsUpdateModuleOpen(false)}
        courseId={id}
        moduleId={selectedModule.id}
        currentTitle={selectedModule.title}
      />

      <AddLessonModal
        isOpen={isAddLessonOpen}
        onClose={() => setIsAddLessonOpen(false)}
        courseId={id}
        moduleId={selectedModule.id}
      />

      <DeleteLessonModal
        isOpen={isDeleteLessonOpen}
        onClose={() => setIsDeleteLessonOpen(false)}
        courseId={id}
        moduleId={selectedModule.id}
        lessonId={selectedLesson.id}
        lessonTitle={selectedLesson.title}
      />
    </>
  );
}
