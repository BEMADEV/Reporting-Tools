﻿// <copyright>
// Copyright by BEMA Information Technologies
//
// Licensed under the Rock Community License (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.rockrms.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//
using System.Collections.Generic;
using Rock.Plugin;

namespace com.bemaservices.ReportingTools
{
    [MigrationNumber( 6, "1.16.0" )]
    public class FieldTypeConversion : Migration
    {
        /// <summary>
        /// The commands to run to migrate plugin to the specific version
        /// </summary>
        public override void Up()
        {
            Sql(
                string.Format(
                    @"
                        Declare @ReportingFieldsCategoryId int = ( Select Top 1 Id From Category Where Guid = '{0}')
                        Declare @HtmlFieldTypeId int = ( Select Top 1 Id From FieldType Where Guid = '{1}')
                        Declare @LavaFieldTypeId int = ( Select Top 1 Id From FieldType Where Guid = '{2}')

                        Update Attribute
                        Set FieldTypeId = @LavaFieldTypeId
                        Where FieldTypeId = @HtmlFieldTypeId
                        And Id in (
                                    Select AttributeId
                                    From AttributeCategory
                                    Where CategoryId = @ReportingFieldsCategoryId
                                    )

                    "
                    , com.bemaservices.ReportingTools.SystemGuid.Category.BEMA_REPORTING_FIELDS
                    , Rock.SystemGuid.FieldType.HTML
                    ,Rock.SystemGuid.FieldType.LAVA
                    ) 
                );
        }

        /// <summary>
        /// The commands to undo a migration from a specific version
        /// </summary>
        public override void Down()
        {
        }
    }
}

