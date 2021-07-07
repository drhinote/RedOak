
angular.module('ROI', ['ngMaterial'])
    .controller('AppCtrl', function ($scope) { })

    .directive('pageTwoDataGrid', function () {
        return {
            template: `
                <div ng-controller="AppCtrl as appCtrl" ng-cloak="" class="gridListdemoBasicUsage" ng-app="ROI" style="width:880px; height:280px">
            <md-grid-list md-cols-xs="8" md-cols-sm="8" md-cols-md="8" md-cols-gt-md="8" md-row-height-gt-md="4:1" md-row-height="4:1" md-gutter="8px" md-gutter-gt-sm="8px">

                <md-grid-tile md-rowspan="2" md-colspan="2" md-colspan-sm="2" md-colspan-xs="2"></md-grid-tile>
                <md-grid-tile md-colspan=2><h3><%=Data.TestDate%></h3></md-grid-tile>
                <md-grid-tile ng-show="'<%=Data.TestDateHist1%>'" md-colspan="2"><h3><%=Data.TestDateHist1%></h3></md-grid-tile>
                <md-grid-tile ng-show="'<%=Data.TestDateHist2%>'" md-colspan="2"><h3><%=Data.TestDateHist2%></h3></md-grid-tile>

                <md-grid-tile class="roiLightBlue" md-rowspan="1"><h4>Left</h4></md-grid-tile>
                <md-grid-tile class="roiLightBlue" md-rowspan="1"><h4>Right</h4></md-grid-tile>
                <md-grid-tile ng-show="'<%=Data.TestDateHist1%>'" class="roiLightBlue" md-rowspan="1"><h4>Left</h4></md-grid-tile>
                <md-grid-tile ng-show="'<%=Data.TestDateHist1%>'" class="roiLightBlue" md-rowspan="1"><h4>Right</h4></md-grid-tile>
                <md-grid-tile ng-show="'<%=Data.TestDateHist2%>'" class="roiLightBlue" md-rowspan="1"><h4>Left</h4></md-grid-tile>
                <md-grid-tile ng-show="'<%=Data.TestDateHist2%>'" class="roiLightBlue" md-rowspan="1"><h4>Right</h4></md-grid-tile>

                <md-grid-tile class="roiLightBlue" md-rowspan="1" md-colspan="2"><h4>Fatigue Varience</h4></md-grid-tile>
                <md-grid-tile ng-style="{'background':'<%=FatigueVariance(Data.LeftFatigueVariance)%>'}" md-rowspan="1"><%=Data.LeftFatigueVariance%></md-grid-tile>
                <md-grid-tile ng-style="{'background':'<%=FatigueVariance(Data.RightFatigueVariance)%>'}" md-rowspan="1"><%=Data.RightFatigueVariance%></md-grid-tile>
                <md-grid-tile ng-show="'<%=Data.LeftHist1.FatigueVarience%>'" ng-style="{'background':'<%=FatigueVariance(Data.LeftHist1.FatigueVarience)%>'}" md-rowspan="1"><%=Data.LeftHist1.FatigueVarience%></md-grid-tile>
                <md-grid-tile ng-show="'<%=Data.RightHist1.FatigueVarience%>'" ng-style="{'background':'<%=FatigueVariance(Data.RightHist1.FatigueVarience)%>'}" md-rowspan="1"><%=Data.RightHist1.FatigueVarience%></md-grid-tile>
                <md-grid-tile ng-show="'<%=Data.LeftHist2.FatigueVarience%>'" ng-style="{'background':'<%=FatigueVariance(Data.LeftHist2.FatigueVarience)%>'}" md-rowspan="1"><%=Data.LeftHist2.FatigueVarience%></md-grid-tile>
                <md-grid-tile ng-show="'<%=Data.RightHist2.FatigueVarience%>'" ng-style="{'background':'<%=FatigueVariance(Data.RightHist2.FatigueVarience)%>'}" md-rowspan="1"><%=Data.RightHist2.FatigueVarience%></md-grid-tile>

                <md-grid-tile class="roiLightBlue" md-rowspan="1" md-colspan="2"><h4>Strength</h4></md-grid-tile>
                <md-grid-tile md-rowspan="1"><%=Data.LeftStrength%></md-grid-tile>
                <md-grid-tile md-rowspan="1"><%=Data.RightStrength%></md-grid-tile>
                <md-grid-tile ng-show="'<%=Data.LeftHist1.Strength%>'" md-rowspan="1"><%=Data.LeftHist1.Strength%></md-grid-tile>
                <md-grid-tile ng-show="'<%=Data.RightHist1.Strength%>'" md-rowspan="1"><%=Data.RightHist1.Strength%></md-grid-tile>
                <md-grid-tile ng-show="'<%=Data.LeftHist2.Strength%>'" md-rowspan="1"><%=Data.LeftHist2.Strength%></md-grid-tile>
                <md-grid-tile ng-show="'<%=Data.RightHist2.Strength%>'" md-rowspan="1"><%=Data.RightHist2.Strength%></md-grid-tile>

                <md-grid-tile class="roiLightBlue" md-rowspan="1" md-colspan="2"><h4>Strength Ratio</h4></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.LeftStrengthRatio%>" md-rowspan="1"><%=Data.LeftStrengthRatio%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.RightStrengthRatio%>" md-rowspan="1"><%=Data.RightStrengthRatio%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.LeftHist1.StrengthRatio%>" md-rowspan="1"><%=Data.LeftHist1.StrengthRatio%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.RightHist1.StrengthRatio%>" md-rowspan="1"><%=Data.RightHist1.StrengthRatio%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.LeftHist2.StrengthRatio%>" md-rowspan="1"><%=Data.LeftHist2.StrengthRatio%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.RightHist2.StrengthRatio%>" md-rowspan="1"><%=Data.RightHist2.StrengthRatio%></md-grid-tile>

                <md-grid-tile class="roiLightBlue" md-rowspan="1" md-colspan="2"><h4>Motor Control</h4></md-grid-tile>
                <md-grid-tile ng-style="{'background':'<%=PercentColor(Data.LeftMotorControl)%>'}" md-rowspan="1"><%=Data.LeftMotorControl.ToString("p0")%></md-grid-tile>
                <md-grid-tile ng-style="{'background':'<%=PercentColor(Data.RightMotorControl)%>'}" md-rowspan="1"><%=Data.RightMotorControl.ToString("p0")%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.LeftHist1.MotorControl.Value%>" ng-style="{'background':'<%=PercentColor(Data.LeftHist1.MotorControl.Value)%>'}" md-rowspan="1"><%=Data.LeftHist1.MotorControl.Value.ToString("p0")%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.RightHist1.MotorControl.Value%>" ng-style="{'background':'<%=PercentColor(Data.RightHist1.MotorControl.Value)%>'}" md-rowspan="1"><%=Data.RightHist1.MotorControl.Value.ToString("p0")%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.LeftHist2.MotorControl.Value%>" ng-style="{'background':'<%=PercentColor(Data.LeftHist2.MotorControl.Value)%>'}" md-rowspan="1"><%=Data.LeftHist2.MotorControl.Value.ToString("p0")%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.RightHist2.MotorControl.Value%>" ng-style="{'background':'<%=PercentColor(Data.RightHist2.MotorControl.Value)%>'}" md-rowspan="1"><%=Data.RightHist2.MotorControl.Value.ToString("p0")%></md-grid-tile>

                <md-grid-tile class="roiLightBlue" md-rowspan="1" md-colspan="2"><h4>Sensory Control</h4></md-grid-tile>
                <md-grid-tile ng-style="{'background':'<%=PercentColor(Data.LeftSensoryControl)%>'}" md-rowspan="1"><%=Data.LeftSensoryControl.ToString("p0")%></md-grid-tile>
                <md-grid-tile ng-style="{'background':'<%=PercentColor(Data.RightSensoryControl)%>'}" md-rowspan="1"><%=Data.RightSensoryControl.ToString("p0")%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.LeftHist1.SensoryControl.Value%>" ng-style="{'background':'<%=PercentColor(Data.LeftHist1.SensoryControl.Value)%>'}" md-rowspan="1"><%=Data.LeftHist1.SensoryControl.Value.ToString("p0")%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.RightHist1.SensoryControl.Value%>" ng-style="{'background':'<%=PercentColor(Data.RightHist1.SensoryControl.Value)%>'}" md-rowspan="1"><%=Data.RightHist1.SensoryControl.Value.ToString("p0")%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.LeftHist2.SensoryControl.Value%>" ng-style="{'background':'<%=PercentColor(Data.LeftHist2.SensoryControl.Value)%>'}" md-rowspan="1"><%=Data.LeftHist2.SensoryControl.Value.ToString("p0")%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.RightHist2.SensoryControl.Value%>" ng-style="{'background':'<%=PercentColor(Data.RightHist2.SensoryControl.Value)%>'}" md-rowspan="1"><%=Data.RightHist2.SensoryControl.Value.ToString("p0")%></md-grid-tile>

                <md-grid-tile class="roiLightBlue" md-rowspan="1" md-colspan="2"><h4>Reaction Time</h4></md-grid-tile>
                <md-grid-tile md-rowspan="1"><%=Data.LeftReactionTime%></md-grid-tile>
                <md-grid-tile md-rowspan="1"><%=Data.RightReactionTime%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.LeftHist1.ReactionTime%>" md-rowspan="1"><%=Data.LeftHist1.ReactionTime%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.RightHist1.ReactionTime%>" md-rowspan="1"><%=Data.RightHist1.ReactionTime%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.LeftHist2.ReactionTime%>" md-rowspan="1"><%=Data.LeftHist2.ReactionTime%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.RightHist2.ReactionTime%>" md-rowspan="1"><%=Data.RightHist2.ReactionTime%></md-grid-tile>

                <md-grid-tile class="roiLightBlue" md-rowspan="1" md-colspan="2"><h4>Cognitive Reaction Time</h4></md-grid-tile>
                <md-grid-tile md-rowspan="1"><%=Data.LeftCognitiveReactionTime%></md-grid-tile>
                <md-grid-tile md-rowspan="1"><%=Data.RightCognitiveReactionTime%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.LeftHist1.CognitiveReactionTime%>" md-rowspan="1"><%=Data.LeftHist1.CognitiveReactionTime%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.RightHist1.CognitiveReactionTime%>" md-rowspan="1"><%=Data.RightHist1.CognitiveReactionTime%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.LeftHist2.CognitiveReactionTime%>" md-rowspan="1"><%=Data.LeftHist2.CognitiveReactionTime%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.RightHist2.CognitiveReactionTime%>" md-rowspan="1"><%=Data.RightHist2.CognitiveReactionTime%></md-grid-tile>

                <md-grid-tile class="roiLightBlue" md-rowspan="1" md-colspan="2"><h4>Correlation</h4></md-grid-tile>
                <md-grid-tile ng-style="{'background':'<%=PercentColor(Data.LeftCorrelation)%>'}" md-rowspan="1"><%=Data.LeftCorrelation.ToString("p0")%></md-grid-tile>
                <md-grid-tile ng-style="{'background':'<%=PercentColor(Data.RightCorrelation)%>'}" md-rowspan="1"><%=Data.RightCorrelation.ToString("p0")%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.LeftHist1.Correlation.Value%>" ng-style="{'background':'<%=PercentColor(Data.LeftHist1.Correlation.Value)%>'}" md-rowspan="1"><%=Data.LeftHist1.Correlation.Value.ToString("p0")%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.RightHist1.Correlation.Value%>" ng-style="{'background':'<%=PercentColor(Data.RightHist1.Correlation.Value)%>'}" md-rowspan="1"><%=Data.RightHist1.Correlation.Value.ToString("p0")%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.LeftHist2.Correlation.Value%>" ng-style="{'background':'<%=PercentColor(Data.LeftHist2.Correlation.Value)%>'}" md-rowspan="1"><%=Data.LeftHist2.Correlation.Value.ToString("p0")%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.RightHist2.Correlation.Value%>" ng-style="{'background':'<%=PercentColor(Data.RightHist2.Correlation)%>'}" md-rowspan="1"><%=Data.RightHist2.Correlation.Value.ToString("p0")%></md-grid-tile>

            </md-grid-list>`
        };
    });