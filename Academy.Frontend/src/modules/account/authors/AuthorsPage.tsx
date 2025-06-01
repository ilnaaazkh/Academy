import {
  Alert,
  Button,
  CircularProgress,
  TextField,
  Typography,
} from "@mui/material";
import { useState } from "react";
import RegisterAuthorModal from "./RegisterAuthorModal";
import AuthorsList from "./AuthorsList";
import { useGetAuthorsQuery } from "./api";

export default function AuthorsPage() {
  const [open, setOpen] = useState(false);
  const [search, setSearch] = useState("");

  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);

  const { data, isLoading, isError } = useGetAuthorsQuery({ search });

  if (isLoading) {
    return (
      <div className="flex justify-center my-10">
        <CircularProgress />
      </div>
    );
  }

  if (isError) {
    return (
      <Alert severity="error" className="my-10">
        Не удалось загрузить список авторов
      </Alert>
    );
  }

  return (
    <div className="p-6 space-y-6">
      <Typography variant="h3">Авторы</Typography>

      <div className="flex justify-between">
        <div>
          <TextField
            label="Поиск по email или ФИО"
            variant="standard"
            size="small"
            className="w-52"
            value={search}
            onChange={(e) => setSearch(e.target.value)}
          />
        </div>
        <Button variant="contained" onClick={handleOpen}>
          Зарегистрировать автора
        </Button>
      </div>

      <AuthorsList authors={data?.result} />

      <RegisterAuthorModal isOpen={open} onClose={handleClose} />
    </div>
  );
}
