import { ClaimListRelationFilter } from "../claim/ClaimListRelationFilter";
import { StringFilter } from "../../util/StringFilter";
import { StringNullableFilter } from "../../util/StringNullableFilter";

export type ClaimStatusWhereInput = {
  claims?: ClaimListRelationFilter;
  id?: StringFilter;
  status?: StringNullableFilter;
};
