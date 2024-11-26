/*
------------------------------------------------------------------------------ 
This code was generated by Amplication. 
 
Changes to this file will be lost if the code is regenerated. 

There are other ways to to customize your code, see this doc to learn more
https://docs.amplication.com/how-to/custom-code

------------------------------------------------------------------------------
  */
import { InputType, Field } from "@nestjs/graphql";
import { ApiProperty } from "@nestjs/swagger";
import { ClaimStatusWhereInput } from "./ClaimStatusWhereInput";
import { ValidateNested, IsOptional } from "class-validator";
import { Type } from "class-transformer";

@InputType()
class ClaimStatusListRelationFilter {
  @ApiProperty({
    required: false,
    type: () => ClaimStatusWhereInput,
  })
  @ValidateNested()
  @Type(() => ClaimStatusWhereInput)
  @IsOptional()
  @Field(() => ClaimStatusWhereInput, {
    nullable: true,
  })
  every?: ClaimStatusWhereInput;

  @ApiProperty({
    required: false,
    type: () => ClaimStatusWhereInput,
  })
  @ValidateNested()
  @Type(() => ClaimStatusWhereInput)
  @IsOptional()
  @Field(() => ClaimStatusWhereInput, {
    nullable: true,
  })
  some?: ClaimStatusWhereInput;

  @ApiProperty({
    required: false,
    type: () => ClaimStatusWhereInput,
  })
  @ValidateNested()
  @Type(() => ClaimStatusWhereInput)
  @IsOptional()
  @Field(() => ClaimStatusWhereInput, {
    nullable: true,
  })
  none?: ClaimStatusWhereInput;
}
export { ClaimStatusListRelationFilter as ClaimStatusListRelationFilter };
