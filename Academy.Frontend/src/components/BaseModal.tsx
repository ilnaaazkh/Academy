// shared/components/BaseModal.tsx

import { Modal, Box, Typography, IconButton } from "@mui/material";
import CloseIcon from "@mui/icons-material/Close";
import { ReactNode } from "react";

interface BaseModalProps {
  open: boolean;
  onClose: () => void;
  title?: string;
  children: ReactNode;
}

export default function BaseModal({
  open,
  onClose,
  title,
  children,
}: BaseModalProps) {
  return (
    <Modal open={open} onClose={onClose} aria-labelledby="base-modal-title">
      <Box className="bg-white rounded-lg shadow-lg p-6 absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2 w-[90%] max-w-lg">
        <Box className="flex justify-between items-center mb-4">
          {title && (
            <Typography id="base-modal-title" variant="h6">
              {title}
            </Typography>
          )}
          <IconButton onClick={onClose}>
            <CloseIcon />
          </IconButton>
        </Box>
        <div>{children}</div>
      </Box>
    </Modal>
  );
}
