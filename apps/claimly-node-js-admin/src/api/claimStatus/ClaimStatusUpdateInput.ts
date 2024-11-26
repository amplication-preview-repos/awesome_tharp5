import { ClaimUpdateManyWithoutClaimStatusesInput } from "./ClaimUpdateManyWithoutClaimStatusesInput";

export type ClaimStatusUpdateInput = {
  claims?: ClaimUpdateManyWithoutClaimStatusesInput;
  status?: string | null;
};
