import * as common from "@nestjs/common";
import * as swagger from "@nestjs/swagger";
import * as nestAccessControl from "nest-access-control";
import { ClaimService } from "./claim.service";
import { ClaimControllerBase } from "./base/claim.controller.base";

@swagger.ApiTags("claims")
@common.Controller("claims")
export class ClaimController extends ClaimControllerBase {
  constructor(
    protected readonly service: ClaimService,
    @nestAccessControl.InjectRolesBuilder()
    protected readonly rolesBuilder: nestAccessControl.RolesBuilder
  ) {
    super(service, rolesBuilder);
  }
}
