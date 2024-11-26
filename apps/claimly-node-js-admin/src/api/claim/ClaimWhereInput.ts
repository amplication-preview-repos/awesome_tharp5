import { FloatNullableFilter } from "../../util/FloatNullableFilter";
import { ClaimStatusWhereUniqueInput } from "../claimStatus/ClaimStatusWhereUniqueInput";
import { CustomerWhereUniqueInput } from "../customer/CustomerWhereUniqueInput";
import { DateTimeNullableFilter } from "../../util/DateTimeNullableFilter";
import { StringNullableFilter } from "../../util/StringNullableFilter";
import { StringFilter } from "../../util/StringFilter";

export type ClaimWhereInput = {
  amount?: FloatNullableFilter;
  claimStatus?: ClaimStatusWhereUniqueInput;
  customer?: CustomerWhereUniqueInput;
  date?: DateTimeNullableFilter;
  description?: StringNullableFilter;
  id?: StringFilter;
};
