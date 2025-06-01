import { Button, Typography } from "@mui/material";
import BaseModal from "../../../components/BaseModal";

interface Props {
  isOpen: boolean;
  onClose: () => void;
  onConfirm: () => void;
}

export default function DeleteCourseModal({
  isOpen,
  onClose,
  onConfirm,
}: Props) {
  return (
    <BaseModal open={isOpen} title="Удаление курса" onClose={onClose}>
      <Typography variant="subtitle1">
        Вы действительно хотите удалить этот курс
      </Typography>
      <Button color="error" variant="contained" onClick={onConfirm}>
        Удалить
      </Button>
    </BaseModal>
  );
}
