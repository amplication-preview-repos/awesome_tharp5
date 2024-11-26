import { Claim as TClaim } from "../api/claim/Claim";

export const CLAIM_TITLE_FIELD = "id";

export const ClaimTitle = (record: TClaim): string => {
  return record.id?.toString() || String(record.id);
};
