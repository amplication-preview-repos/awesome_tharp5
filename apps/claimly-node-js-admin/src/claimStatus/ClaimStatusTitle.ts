import { ClaimStatus as TClaimStatus } from "../api/claimStatus/ClaimStatus";

export const CLAIMSTATUS_TITLE_FIELD = "status";

export const ClaimStatusTitle = (record: TClaimStatus): string => {
  return record.status?.toString() || String(record.id);
};
